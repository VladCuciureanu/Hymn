import { Injectable } from '@nestjs/common';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { PrismaService } from 'src/prisma/prisma.service';
import argon2 from 'argon2';
import { User } from '@prisma/client';
import { UserEntity, UserEntityWithPasswordHash } from './entities/user.entity';
import { deletePropertyFromObject } from 'src/utils/delete-property-from-object';

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

    const censoredEntities = entities.map(censorUser);

    return censoredEntities;
  }

  async findOne(id: string): Promise<UserEntity | null> {
    const entity = await this.prisma.user.findUnique({ where: { id: id } });
    if (!entity) return null;
    const censoredEntity = censorUser(entity);
    return censoredEntity;
  }

  async findOneByEmail(
    email: string,
  ): Promise<UserEntityWithPasswordHash | null> {
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

function censorUser(user: User): UserEntity {
  return deletePropertyFromObject(user, 'passwordHash');
}
