import {
  Injectable,
  NotFoundException,
  UnauthorizedException,
} from '@nestjs/common';
import { JwtService } from '@nestjs/jwt';
import { UsersService } from 'src/users/users.service';
import { LoginDto } from './dto/login.dto';
import { verify } from 'argon2';
import { JwtPayload } from './interfaces/jwt-payload.interface';
import { deletePropertyFromObject } from 'src/utils/delete-property-from-object';

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
      user: deletePropertyFromObject(user, 'passwordHash'),
      sub: user.id,
    };

    return {
      access_token: this.jwtService.sign(payload),
    };
  }
}
