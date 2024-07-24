import { Test, TestingModule } from '@nestjs/testing';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { PrismaModule } from '../prisma/prisma.module';
import { UserEntity } from './entities/user.entity';
import { UserRole } from '@prisma/client';
import { AuthenticatedRequest } from '../auth/interfaces/authenticated-request.interface';

const mockUser: UserEntity = {
  id: 'mockId',
  email: 'mockEmail',
  username: 'mockUsername',
  passwordHash: 'mockPasswordHash',
  roles: [UserRole.Member],
};

const mockAuthenticatedRequest: AuthenticatedRequest = {
  user: mockUser,
} as any;

describe('UsersController', () => {
  let controller: UsersController;
  let service: UsersService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [UsersController],
      providers: [UsersService],
      imports: [PrismaModule],
    }).compile();

    controller = module.get<UsersController>(UsersController);
    service = module.get<UsersService>(UsersService);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });

  describe('create', () => {
    it('should create a user', async () => {
      jest.spyOn(service, 'create').mockResolvedValue(mockUser);

      const user = await controller.create({
        email: 'test@email.com',
        password: 'password',
        username: 'testusername',
      });

      expect(user).toBeDefined();
    });
  });

  describe('findAll', () => {
    it('should return an array of users', async () => {
      jest.spyOn(service, 'findAll').mockResolvedValue([mockUser]);

      const users = await controller.findAll(mockAuthenticatedRequest);

      expect(users).toEqual([mockUser]);
    });
  });

  describe('findOne', () => {
    it('should return a user', async () => {
      jest.spyOn(service, 'findOne').mockResolvedValue(mockUser);

      const user = await controller.findOne(
        mockAuthenticatedRequest,
        mockUser.id,
      );

      expect(user).toEqual(mockUser);
    });
  });

  describe('update', () => {
    it('should update a user', async () => {
      const updatedUser = { ...mockUser, email: 'newEmail' };
      jest.spyOn(service, 'update').mockResolvedValue(updatedUser);

      const user = await controller.update(
        mockAuthenticatedRequest,
        mockUser.id,
        {
          email: updatedUser.email,
        },
      );

      expect(user).toEqual(updatedUser);
    });
  });

  describe('remove', () => {
    it('should remove a user', async () => {
      jest.spyOn(service, 'remove').mockResolvedValue(mockUser);

      const user = await controller.remove(
        mockAuthenticatedRequest,
        mockUser.id,
      );

      expect(user).toEqual(mockUser);
    });
  });
});
