import {
  IsAlphanumeric,
  IsEmail,
  IsString,
  IsStrongPassword,
  MinLength,
} from 'class-validator';

export class CreateUserDto {
  @IsEmail()
  readonly email: string;

  @IsString()
  @IsAlphanumeric()
  @MinLength(4)
  readonly username: string;

  @IsStrongPassword()
  readonly password: string;
}
