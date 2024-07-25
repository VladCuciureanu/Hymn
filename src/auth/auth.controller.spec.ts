import { Test, TestingModule } from '@nestjs/testing';
import { AuthController } from './auth.controller';
import { AuthService } from './auth.service';
import { UsersModule } from '../users/users.module';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { JwtModule } from '@nestjs/jwt';
import { Response } from 'express';

const mockPayload = { access_token: '' };
const mockResponse: Response<any, Record<string, any>> = {
  cookie: jest.fn(),
} as any;

describe('AuthController', () => {
  let controller: AuthController;
  let service: DeepMockProxy<AuthService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [AuthController],
      providers: [AuthService],
      imports: [UsersModule, JwtModule],
    })
      .overrideProvider(AuthService)
      .useValue(mockDeep(AuthService))
      .compile();

    controller = module.get<AuthController>(AuthController);
    service = module.get<DeepMockProxy<AuthService>>(AuthService);
  });

  describe('login', () => {
    it('should return a token when logging in', async () => {
      jest.spyOn(service, 'login').mockResolvedValueOnce(mockPayload);

      const payload = await controller.login(
        { email: '', password: '' },
        mockResponse,
      );

      expect(service.login).toHaveBeenCalled();
      expect(payload).toEqual(mockPayload);
    });
  });

  describe('register', () => {
    it('should return a token when registering', async () => {
      jest.spyOn(service, 'register').mockResolvedValueOnce(mockPayload);

      const payload = await controller.register(
        { email: '', username: '', password: '' },
        mockResponse,
      );

      expect(service.register).toHaveBeenCalled();
      expect(payload).toEqual(mockPayload);
    });
  });
});
