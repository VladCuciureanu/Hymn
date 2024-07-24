import { Test, TestingModule } from '@nestjs/testing';
import { UsersController } from './users.controller';
import { UsersService } from './users.service';
import { PrismaModule } from '../prisma/prisma.module';
import { UserEntity } from './entities/user.entity';
import { UserRole } from '@prisma/client';
import { AuthenticatedRequest } from '../auth/interfaces/authenticated-request.interface';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { CreateUserDto } from './dto/create-user.dto';

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
      jest.spyOn(service, 'create').mockResolvedValueOnce(mockUser);

      const dto: CreateUserDto = {
        email: mockUser.email,
        username: mockUser.username,
        password: 'password',
      };

      const user = await controller.create(dto);

      expect(service.create).toHaveBeenCalledWith(dto);
      expect(user).toBeDefined();
    });
  });

  describe('findAll', () => {
    it('should return an array of users', async () => {
      jest.spyOn(service, 'findAll').mockResolvedValueOnce([mockUser]);

      const users = await controller.findAll(mockAuthenticatedRequest);

      expect(service.findAll).toHaveBeenCalled();
      expect(users).toEqual([mockUser]);
    });
  });

  describe('findOne', () => {
    it('should return a user', async () => {
      jest.spyOn(service, 'findOne').mockResolvedValueOnce(mockUser);

      const user = await controller.findOne(
        mockAuthenticatedRequest,
        mockUser.id,
      );

      expect(service.findOne).toHaveBeenCalled();
      expect(user).toEqual(mockUser);
    });
  });

  describe('update', () => {
    it('should update a user', async () => {
      const updatedUser = { ...mockUser, email: 'newEmail' };
      jest.spyOn(service, 'update').mockResolvedValueOnce(updatedUser);

      const user = await controller.update(
        mockAuthenticatedRequest,
        mockUser.id,
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
      jest.spyOn(service, 'remove').mockResolvedValueOnce(mockUser);

      const user = await controller.remove(
        mockAuthenticatedRequest,
        mockUser.id,
      );

      expect(service.remove).toHaveBeenCalled();
      expect(user).toEqual(mockUser);
    });
  });
});
