import {
  Injectable,
  NotFoundException,
  UnauthorizedException,
} from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UsersService } from '../users/users.service';
import { LoginDto } from './dto/login.dto';
import { verify } from 'argon2';
import { JwtPayload } from './interfaces/jwt-payload.interface';
import { REDACTED_STRING } from './auth.constants';

@Injectable()
export class AuthService {
  constructor(
    private usersService: UsersService,
    private jwtService: JwtService,
  ) {}

  async login(credentials: LoginDto) {
    const user = await this.usersService.findOneByEmail(credentials.email);

    if (!user) {
      return new NotFoundException();
    }

    const isPasswordMatching = await verify(
      user.passwordHash,
      credentials.password,
    );

    if (!isPasswordMatching) {
      return new UnauthorizedException();
    }

    const payload: JwtPayload = {
      user: { ...user, passwordHash: REDACTED_STRING },
      sub: user.id,
    };

    return {
      access_token: this.jwtService.sign(payload),
    };
  }
}
