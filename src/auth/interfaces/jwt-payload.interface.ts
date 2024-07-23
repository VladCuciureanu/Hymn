import { UserEntity } from 'src/users/entities/user.entity';

export interface JwtPayload {
  user: UserEntity | null;
  sub: string;
}
