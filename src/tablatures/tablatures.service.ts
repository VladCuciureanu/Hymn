import { Injectable, NotFoundException } from '@nestjs/common';
import { CreateTablatureDto } from './dto/create-tablature.dto';
import { UpdateTablatureDto } from './dto/update-tablature.dto';
import { PrismaService } from '../prisma/prisma.service';
import { TablatureEntity } from './entities/tablature.entity';
import { UserEntity } from 'src/users/entities/user.entity';
import { TablatureStatus, UserRole } from '@prisma/client';

@Injectable()
export class TablaturesService {
  constructor(private prisma: PrismaService) {}

  async create(props: {
    dto: CreateTablatureDto;
    accountability: UserEntity;
  }): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.create({
      data: {
        ...props.dto,
        createdById: props.accountability.id,
        updatedById: props.accountability.id,
      },
    });

    return entity;
  }

  async findAll(props: {
    accountability?: UserEntity;
  }): Promise<TablatureEntity[]> {
    const isAdmin = props.accountability?.roles.includes(UserRole.Admin);

    const entities = await this.prisma.tablature.findMany({
      where: {
        OR: [
          { status: TablatureStatus.Published },
          {
            status: TablatureStatus.Draft,
            createdById: !isAdmin ? props.accountability?.id : undefined,
          },
        ],
      },
    });

    return entities;
  }

  async findOne(props: {
    id: string;
    accountability?: UserEntity;
  }): Promise<TablatureEntity | null> {
    const isAdmin = props.accountability?.roles.includes(UserRole.Admin);

    const entity = await this.prisma.tablature.findFirst({
      where: {
        AND: [
          { id: props.id },
          {
            OR: [
              { status: TablatureStatus.Published },
              {
                status: TablatureStatus.Draft,
                createdById: !isAdmin ? props.accountability?.id : undefined,
              },
            ],
          },
        ],
      },
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);

    return entity;
  }

  async update(props: {
    id: string;
    dto: UpdateTablatureDto;
    accountability: UserEntity;
  }): Promise<TablatureEntity | null> {
    const isAdmin = props.accountability?.roles.includes(UserRole.Admin);

    const entity = await this.prisma.tablature.update({
      where: {
        id: props.id,
        status: TablatureStatus.Draft,
        createdById: !isAdmin ? props.accountability?.id : undefined,
      },
      data: { ...props.dto, updatedById: props.accountability.id },
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);

    return entity;
  }

  async remove(props: {
    id: string;
    accountability: UserEntity;
  }): Promise<TablatureEntity | null> {
    const isAdmin = props.accountability?.roles.includes(UserRole.Admin);

    const entity = await this.prisma.tablature.delete({
      where: {
        id: props.id,
        status: TablatureStatus.Draft,
        createdById: !isAdmin ? props.accountability?.id : undefined,
      },
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);
    return entity;
  }
}
