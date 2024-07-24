import { Injectable } from '@nestjs/common';
import { CreateTablatureDto } from './dto/create-tablature.dto';
import { UpdateTablatureDto } from './dto/update-tablature.dto';
import { PrismaService } from '../prisma/prisma.service';
import { TablatureEntity } from './entities/tablature.entity';

@Injectable()
export class TablaturesService {
  constructor(private prisma: PrismaService) {}

  async create(
    createTablatureDto: CreateTablatureDto,
  ): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.create({
      data: createTablatureDto,
    });
    return entity;
  }

  async findAll(): Promise<TablatureEntity[]> {
    const entities = await this.prisma.tablature.findMany();

    return entities;
  }

  async findOne(id: string): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.findUnique({
      where: { id: id },
    });

    return entity;
  }

  async update(
    id: string,
    updateTablatureDto: UpdateTablatureDto,
  ): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.update({
      where: { id: id },
      data: updateTablatureDto,
    });
    return entity;
  }

  async remove(id: string): Promise<TablatureEntity | null> {
    const entity = await this.prisma.tablature.delete({ where: { id: id } });
    return entity;
  }
}
