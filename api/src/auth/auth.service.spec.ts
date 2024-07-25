import { Test, TestingModule } from '@nestjs/testing';
import { AuthService } from './auth.service';
import { UsersModule } from '../users/users.module';
import { JwtModule, JwtService } from '@nestjs/jwt';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { UsersService } from '../users/users.service';
import { mockMemberUser, mockUserPassword } from '../constants';
import { getError } from '../utils/get-error';
import { NotFoundException } from '@nestjs/common';

describe('AuthService', () => {
  let authService: AuthService;
  let usersService: DeepMockProxy<UsersService>;
  let jwtService: DeepMockProxy<JwtService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [AuthService],
      imports: [UsersModule, JwtModule],
    })
      .overrideProvider(JwtService)
      .useValue(mockDeep<JwtService>())
      .overrideProvider(UsersService)
      .useValue(mockDeep<UsersService>())
      .compile();

    authService = module.get<AuthService>(AuthService);
    usersService = module.get<DeepMockProxy<UsersService>>(UsersService);
    jwtService = module.get<DeepMockProxy<JwtService>>(JwtService);
  });

  it('should return a token when logging in a valid user', async () => {
    jest
      .spyOn(usersService, 'findOneByEmail')
      .mockResolvedValueOnce(mockMemberUser);
    jest.spyOn(jwtService, 'sign').mockReturnValueOnce('mockToken');

    const payload = await authService.login({
      email: mockMemberUser.email,
      password: mockUserPassword,
    });

    expect(payload.access_token).toBeDefined();
  });

  it("shouldn't return a token when logging in an invalid user", async () => {
    jest.spyOn(usersService, 'findOneByEmail').mockResolvedValueOnce(null);

    const error = await getError(
      async () =>
        await authService.login({
          email: mockMemberUser.email,
          password: mockUserPassword,
        }),
    );

    expect(error).toBeInstanceOf(NotFoundException);
  });
});
