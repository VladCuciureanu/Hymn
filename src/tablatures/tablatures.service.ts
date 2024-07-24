import {
  Injectable,
  NotFoundException,
  UnauthorizedException,
} from '@nestjs/common';
import { CreateTablatureDto } from './dto/create-tablature.dto';
import { UpdateTablatureDto } from './dto/update-tablature.dto';
import { PrismaService } from '../prisma/prisma.service';
import { TablatureEntity } from './entities/tablature.entity';
import { UserEntity } from 'src/users/entities/user.entity';
import { UserRole } from '@prisma/client';

@Injectable()
export class TablaturesService {
  constructor(private prisma: PrismaService) {}

  async create(props: {
    dto: CreateTablatureDto;
    accountability?: UserEntity;
  }): Promise<TablatureEntity | null> {
    if (!props.accountability?.roles.includes(UserRole.Admin)) {
      throw new UnauthorizedException();
    }

    const entity = await this.prisma.tablature.create({
      data: props.dto,
    });
    return entity;
  }

  async findAll(): Promise<TablatureEntity[]> {
    const entities = await this.prisma.tablature.findMany();
    return entities;
  }

  async findOne(props: {
    id: string;
    accountability?: UserEntity;
  }): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.findUnique({
      where: { id: props.id },
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);
    return entity;
  }

  async update(props: {
    id: string;
    dto: UpdateTablatureDto;
    accountability?: UserEntity;
  }): Promise<TablatureEntity | null> {
    if (!props.accountability?.roles.includes(UserRole.Admin)) {
      throw new UnauthorizedException();
    }

    const entity = await this.prisma.tablature.update({
      where: { id: props.id },
      data: props.dto,
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);
    return entity;
  }

  async remove(props: {
    id: string;
    accountability?: UserEntity;
  }): Promise<TablatureEntity | null> {
    if (!props.accountability?.roles.includes(UserRole.Admin)) {
      throw new UnauthorizedException();
    }

    const entity = await this.prisma.tablature.delete({
      where: { id: props.id },
    });

    if (!entity)
      throw new NotFoundException(`Can't find tablature with id ${props.id}`);
    return entity;
  }
}
