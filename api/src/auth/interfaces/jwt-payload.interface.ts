import { UserEntity } from '../../users/entities/user.entity';

export interface JwtPayload {
  user: UserEntity | null;
  sub: string;
}
