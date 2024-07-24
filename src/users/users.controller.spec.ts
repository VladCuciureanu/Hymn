import { Test, TestingModule } from '@nestjs/testing';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { PrismaModule } from '../prisma/prisma.module';
import { AuthenticatedRequest } from '../auth/interfaces/authenticated-request.interface';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { CreateUserDto } from './dto/create-user.dto';
import { mockMemberUser } from '../constants';

const mockAuthenticatedRequest: AuthenticatedRequest = {
  user: mockMemberUser,
} as any;

describe('UsersController', () => {
  let controller: UsersController;
  let service: DeepMockProxy<UsersService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [UsersController],
      providers: [UsersService],
      imports: [PrismaModule],
    })
      .overrideProvider(UsersService)
      .useValue(mockDeep(UsersService))
      .compile();

    controller = module.get<UsersController>(UsersController);
    service = module.get<DeepMockProxy<UsersService>>(UsersService);
  });

  describe('create', () => {
    it('should create a user', async () => {
      jest.spyOn(service, 'create').mockResolvedValueOnce(mockMemberUser);

      const dto: CreateUserDto = {
        email: mockMemberUser.email,
        username: mockMemberUser.username,
        password: 'password',
      };

      const user = await controller.create(dto);

      expect(service.create).toHaveBeenCalled();
      expect(user).toBeDefined();
    });
  });

  describe('findAll', () => {
    it('should return an array of users', async () => {
      jest.spyOn(service, 'findAll').mockResolvedValueOnce([mockMemberUser]);

      const users = await controller.findAll(mockAuthenticatedRequest);

      expect(service.findAll).toHaveBeenCalled();
      expect(users).toEqual([mockMemberUser]);
    });
  });

  describe('findOne', () => {
    it('should return a user', async () => {
      jest.spyOn(service, 'findOne').mockResolvedValueOnce(mockMemberUser);

      const user = await controller.findOne(
        mockAuthenticatedRequest,
        mockMemberUser.id,
      );

      expect(service.findOne).toHaveBeenCalled();
      expect(user).toEqual(mockMemberUser);
    });
  });

  describe('update', () => {
    it('should update a user', async () => {
      const updatedUser = { ...mockMemberUser, email: 'newEmail' };
      jest.spyOn(service, 'update').mockResolvedValueOnce(updatedUser);

      const user = await controller.update(
        mockAuthenticatedRequest,
        mockMemberUser.id,
        {
          email: updatedUser.email,
        },
      );

      expect(service.update).toHaveBeenCalled();
      expect(user).toEqual(updatedUser);
    });
  });

  describe('remove', () => {
    it('should remove a user', async () => {
      jest.spyOn(service, 'remove').mockResolvedValueOnce(mockMemberUser);

      const user = await controller.remove(
        mockAuthenticatedRequest,
        mockMemberUser.id,
      );

      expect(service.remove).toHaveBeenCalled();
      expect(user).toEqual(mockMemberUser);
    });
  });
});
