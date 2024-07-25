import { ApiProperty } from '@nestjs/swagger';
import { User, UserRole } from '@prisma/client';
import { REDACTED_STRING } from '../../constants';
import { IsAlphanumeric, IsEmail, IsString, MinLength } from 'class-validator';

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
  @IsEmail()
  email: string;

  @ApiProperty({
    example: 'johndoe',
    description: "The user's handle name",
  })
  @IsString()
  @IsAlphanumeric()
  @MinLength(4)
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

  @ApiProperty({
    example: new Date(),
    description: "The user's creation date",
  })
  createdAt: Date;

  @ApiProperty({
    example: new Date(),
    description: "The user's last update date",
  })
  updatedAt: Date;
}
