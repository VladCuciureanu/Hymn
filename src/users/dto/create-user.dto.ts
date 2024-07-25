import { ApiProperty, PickType } from '@nestjs/swagger';
import { IsStrongPassword } from 'class-validator';
import { UserEntity } from '../entities/user.entity';

export class CreateUserDto extends PickType(UserEntity, ['email', 'username']) {
  @ApiProperty()
  @IsStrongPassword()
  readonly password: string;
}
