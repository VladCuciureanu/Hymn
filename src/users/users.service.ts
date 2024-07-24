import {
  Injectable,
  NotFoundException,
  UnauthorizedException,
} from '@nestjs/common';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import { PrismaService } from '../prisma/prisma.service';
import { hash } from 'argon2';
import { User, UserRole } from '@prisma/client';
import { UserEntity } from './entities/user.entity';
import { REDACTED_STRING } from '../constants';

@Injectable()
export class UsersService {
  constructor(private prisma: PrismaService) {}

  async create(createUserDto: CreateUserDto): Promise<UserEntity | null> {
    const { password, ...restOfDto } = createUserDto;
    const passwordHash = await hash(password);
    const entity = await this.prisma.user.create({
      data: { ...restOfDto, passwordHash },
    });
    const censoredEntity = censorUser(entity, entity);
    return censoredEntity;
  }

  async findAll(props: { accountability?: UserEntity }): Promise<UserEntity[]> {
    const entities = await this.prisma.user.findMany();

    const censoredEntities = entities.map((entity) =>
      censorUser(entity, props.accountability),
    );

    return censoredEntities;
  }

  async findOne(props: {
    id: string;
    accountability?: UserEntity;
  }): Promise<UserEntity | null> {
    const entity = await this.prisma.user.findUnique({
      where: { id: props.id },
    });
    if (!entity) throw new NotFoundException();
    const censoredEntity = censorUser(entity, props.accountability);
    return censoredEntity;
  }

  async findOneByEmail(props: {
    email: string;
    accountability?: UserEntity;
  }): Promise<UserEntity | null> {
    const entity = await this.prisma.user.findUnique({
      where: { email: props.email },
    });
    return entity;
  }

  async update(props: {
    id: string;
    dto: UpdateUserDto;
    accountability?: UserEntity;
  }): Promise<UserEntity | null> {
    let passwordHash: string | undefined;
    const { password, ...restOfDto } = props.dto;
    if (password) {
      passwordHash = await hash(password);
    }

    if (
      props.accountability?.id !== props.id &&
      !props.accountability?.roles.includes(UserRole.Admin)
    ) {
      throw new UnauthorizedException();
    }

    const entity = await this.prisma.user.update({
      where: { id: props.id },
      data: { ...restOfDto, passwordHash },
    });

    if (!entity)
      throw new NotFoundException(`Can't find user with id ${props.id}`);
    const censoredEntity = censorUser(entity, props.accountability);
    return censoredEntity;
  }

  async remove(props: {
    id: string;
    accountability?: UserEntity;
  }): Promise<UserEntity | null> {
    if (
      props.accountability?.id !== props.id &&
      !props.accountability?.roles.includes(UserRole.Admin)
    ) {
      throw new UnauthorizedException();
    }

    const entity = await this.prisma.user.delete({ where: { id: props.id } });

    if (!entity)
      throw new NotFoundException(`Can't find user with id ${props.id}`);
    const censoredEntity = censorUser(entity, props.accountability);
    return censoredEntity;
  }
}

function censorUser(user: User, accountability?: UserEntity): UserEntity {
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
