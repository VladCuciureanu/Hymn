import {
  ExecutionContext,
  Injectable,
  UnauthorizedException,
} from '@nestjs/common';
import { Reflector } from '@nestjs/core';
import { AuthGuard } from '@nestjs/passport';
import { IS_PUBLIC_KEY } from '../decorators/public.decorator';

@Injectable()
export class JwtAuthGuard extends AuthGuard('jwt') {
  constructor(private readonly reflector: Reflector) {
    super();
  }

  handleRequest(_err: any, user: any, _info: any, context: ExecutionContext) {
    const allowAny = this.reflector.get<string[]>(
      IS_PUBLIC_KEY,
      context.getHandler(),
    );
    if (user) return user;
    if (allowAny) return true;
    throw new UnauthorizedException();
  }
}
