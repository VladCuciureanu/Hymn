import { Injectable } from '@nestjs/common';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { PrismaService } from '../prisma/prisma.service';
import argon2 from 'argon2';
import { User, UserRole } from '@prisma/client';
import { UserEntity } from './entities/user.entity';
import { REDACTED_STRING } from '../constants';

@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}

  async create(createUserDto: CreateUserDto): Promise<UserEntity | null> {
    const passwordHash = await argon2.hash(createUserDto.password);
    const entity = await this.prisma.user.create({
      data: { ...createUserDto, passwordHash },
    });
    const censoredEntity = censorUser(entity);
    return censoredEntity;
  }

  async findAll(): Promise<UserEntity[]> {
    const entities = await this.prisma.user.findMany();

    const censoredEntities = entities.map((entity) => censorUser(entity));

    return censoredEntities;
  }

  async findOne(id: string): Promise<UserEntity | null> {
    const entity = await this.prisma.user.findUnique({ where: { id: id } });
    if (!entity) return null;
    const censoredEntity = censorUser(entity);
    return censoredEntity;
  }

  async findOneByEmail(email: string): Promise<UserEntity | null> {
    const entity = await this.prisma.user.findUnique({
      where: { email: email },
    });
    return entity;
  }

  async update(
    id: string,
    updateUserDto: UpdateUserDto,
  ): Promise<UserEntity | null> {
    let passwordHash: string | undefined;
    if (updateUserDto.password) {
      passwordHash = await argon2.hash(updateUserDto.password);
    }
    const entity = await this.prisma.user.update({
      where: { id: id },
      data: { ...updateUserDto, passwordHash },
    });
    if (!entity) return null;
    const censoredEntity = censorUser(entity);
    return censoredEntity;
  }

  async remove(id: string): Promise<UserEntity | null> {
    const entity = await this.prisma.user.delete({ where: { id: id } });
    if (!entity) return null;
    const censoredEntity = censorUser(entity);
    return censoredEntity;
  }
}

function censorUser(user: User, accountability?: User): UserEntity {
  const censoredUser: UserEntity = {
    ...user,
    passwordHash: REDACTED_STRING,
    email:
      accountability?.id === user.id ||
      accountability?.roles.includes(UserRole.Admin)
        ? user.email
        : REDACTED_STRING,
  };
  return censoredUser;
}
