import { ApiProperty } from '@nestjs/swagger';
import { User, UserRole } from '@prisma/client';
import { REDACTED_STRING } from 'src/auth/auth.constants';

export class UserEntity implements User {
  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The user's unique identifier",
  })
  id: string;

  @ApiProperty({
    example: REDACTED_STRING,
    description: "The user's email address",
  })
  email: string;

  @ApiProperty({
    example: 'johndoe',
    description: "The user's handle name",
  })
  username: string;

  @ApiProperty({
    example: REDACTED_STRING,
    description: "The user's salted and hashed password",
  })
  passwordHash: string;

  @ApiProperty({
    example: [UserRole.Member],
    description: "The user's roles",
  })
  roles: UserRole[];
}
