import { Test, TestingModule } from '@nestjs/testing';
import { UsersService } from './users.service';
import { PrismaModule } from '../prisma/prisma.module';
import { PrismaService } from '../prisma/prisma.service';
import { User, UserRole } from '@prisma/client';
import { REDACTED_STRING } from '../constants';
import { NotFoundException, UnauthorizedException } from '@nestjs/common';
import { getError } from '../utils/get-error';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';

const mockMemberUser: User = {
  id: 'mockIdMember',
  email: 'mockEmailMember',
  username: 'mockUsernameMember',
  passwordHash: 'mockPasswordHash',
  roles: [UserRole.Member],
};

const mockAdminUser: User = {
  id: 'mockIdAdmin',
  email: 'mockEmailAdmin',
  username: 'mockUsernameAdmin',
  passwordHash: 'mockPasswordHash',
  roles: [UserRole.Member, UserRole.Admin],
};

describe('UsersService', () => {
  let service: UsersService;
  let db: DeepMockProxy<PrismaService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [UsersService],
      imports: [PrismaModule],
    })
      .overrideProvider(PrismaService)
      .useValue(mockDeep<PrismaService>())
      .compile();

    service = module.get<UsersService>(UsersService);
    db = module.get<DeepMockProxy<PrismaService>>(PrismaService);
  });

  it('should create a user', async () => {
    jest.spyOn(db.user, 'create').mockResolvedValueOnce(mockMemberUser);

    const user = await service.create({
      email: mockMemberUser.email,
      username: mockMemberUser.username,
      password: 'password',
    });

    expect(user).toEqual({ ...mockMemberUser, passwordHash: REDACTED_STRING });
  });

  it('should return a member an array of censored users', async () => {
    jest
      .spyOn(db.user, 'findMany')
      .mockResolvedValueOnce([mockMemberUser, mockAdminUser]);

    const users = await service.findAll({ accountability: mockMemberUser });

    expect(users).toEqual([
      { ...mockMemberUser, passwordHash: REDACTED_STRING },
      {
        ...mockAdminUser,
        passwordHash: REDACTED_STRING,
        email: REDACTED_STRING,
      },
    ]);
  });

  it('should return an admin an array of uncensored users', async () => {
    jest
      .spyOn(db.user, 'findMany')
      .mockResolvedValueOnce([mockMemberUser, mockAdminUser]);

    const users = await service.findAll({ accountability: mockAdminUser });

    expect(users).toEqual([
      { ...mockMemberUser, passwordHash: REDACTED_STRING },
      { ...mockAdminUser, passwordHash: REDACTED_STRING },
    ]);
  });

  it('should return an anonymous user a censored user', async () => {
    jest.spyOn(db.user, 'findUnique').mockResolvedValueOnce(mockAdminUser);

    const user = await service.findOne({
      id: mockAdminUser.id,
    });

    expect(user).toEqual({
      ...mockAdminUser,
      email: REDACTED_STRING,
      passwordHash: REDACTED_STRING,
    });
  });

  it('should return a member a censored user', async () => {
    jest.spyOn(db.user, 'findUnique').mockResolvedValueOnce(mockAdminUser);

    const user = await service.findOne({
      id: mockAdminUser.id,
      accountability: mockMemberUser,
    });

    expect(user).toEqual({
      ...mockAdminUser,
      email: REDACTED_STRING,
      passwordHash: REDACTED_STRING,
    });
  });

  it('should return an admin an uncensored user', async () => {
    jest.spyOn(db.user, 'findUnique').mockResolvedValueOnce(mockMemberUser);

    const user = await service.findOne({
      id: mockMemberUser.id,
      accountability: mockAdminUser,
    });

    expect(user).toEqual({
      ...mockMemberUser,
      passwordHash: REDACTED_STRING,
    });
  });

  it("shouldn't return a non-existent user", async () => {
    jest.spyOn(db.user, 'findUnique').mockResolvedValueOnce(null);

    const error = await getError(async () => {
      await service.findOne({
        id: mockAdminUser.id,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });

  it("should update a user's info", async () => {
    const newMockEmail = 'newEmail';

    jest
      .spyOn(db.user, 'update')
      .mockResolvedValueOnce({ ...mockMemberUser, email: newMockEmail });

    const user = await service.update({
      id: mockMemberUser.id,
      dto: { email: newMockEmail },
      accountability: mockMemberUser,
    });

    expect(user).toEqual({
      ...mockMemberUser,
      email: newMockEmail,
      passwordHash: REDACTED_STRING,
    });
  });

  it("shouldn't update another user's account", async () => {
    const error = await getError(async () => {
      await service.update({
        id: mockAdminUser.id,
        dto: { email: '' },
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(UnauthorizedException);
  });

  it("shouldn't update non-existent users", async () => {
    jest
      .spyOn(db.user, 'update')
      .withImplementation((async () => null) as any, () => {});

    const error = await getError(async () => {
      await service.update({
        id: mockMemberUser.id,
        dto: { email: '' },
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });

  it("should update another user's account if admin", async () => {
    const newMockEmail = 'newEmail';

    jest
      .spyOn(db.user, 'update')
      .mockResolvedValueOnce({ ...mockMemberUser, email: newMockEmail });

    const user = await service.update({
      id: mockMemberUser.id,
      dto: { email: newMockEmail },
      accountability: mockAdminUser,
    });

    expect(user).toEqual({
      ...mockMemberUser,
      email: newMockEmail,
      passwordHash: REDACTED_STRING,
    });
  });

  it("should delete a user's account", async () => {
    jest.spyOn(db.user, 'delete').mockResolvedValueOnce(mockMemberUser);

    const user = await service.remove({
      id: mockMemberUser.id,
      accountability: mockMemberUser,
    });

    expect(user).toEqual({ ...mockMemberUser, passwordHash: REDACTED_STRING });
  });

  it("shouldn't delete another user's account", async () => {
    const error = await getError(async () => {
      await service.remove({
        id: mockAdminUser.id,
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(UnauthorizedException);
  });

  it("shouldn't delete non-existent users", async () => {
    jest.spyOn(db.user, 'delete').withImplementation(
      () => null as any,
      () => {},
    );

    const error = await getError(async () => {
      await service.remove({
        id: mockMemberUser.id,
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });

  it("should delete another user's account if admin", async () => {
    jest.spyOn(db.user, 'delete').mockResolvedValueOnce(mockMemberUser);

    const user = await service.remove({
      id: mockMemberUser.id,
      accountability: mockAdminUser,
    });

    expect(user).toEqual({ ...mockMemberUser, passwordHash: REDACTED_STRING });
  });
});
