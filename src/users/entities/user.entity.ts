import { ApiProperty, OmitType } from '@nestjs/swagger';
import { User, UserRole } from '@prisma/client';

export class UserEntityWithPasswordHash implements User {
  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The user's unique identifier",
  })
  id: string;

  @ApiProperty({
    example: 'john@doe.com',
    description: "The user's email address",
  })
  email: string;

  @ApiProperty({
    example: 'johndoe',
    description: "The user's handle name",
  })
  username: string;

  passwordHash: string;

  @ApiProperty({
    example: [UserRole.Member],
    description: "The user's roles",
  })
  roles: UserRole[];
}

export class UserEntity extends OmitType(UserEntityWithPasswordHash, [
  'passwordHash',
]) {}
