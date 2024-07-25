import { ApiProperty } from '@nestjs/swagger';
import { Tablature, TablatureStatus } from '@prisma/client';
import { IsAlphanumeric, IsEnum, IsString, MinLength } from 'class-validator';

export class TablatureEntity implements Tablature {
  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The tablature's unique identifier",
  })
  id: string;

  @ApiProperty({
    example: 'Lorem Ipsum',
    description: "The tablature's title",
  })
  @IsString()
  @IsAlphanumeric()
  @MinLength(4)
  title: string;

  @ApiProperty({
    enum: TablatureStatus,
    example: TablatureStatus.Draft,
    description: "The tablature's status",
  })
  @IsEnum(TablatureStatus)
  status: TablatureStatus;

  @ApiProperty({
    example: new Date(),
    description: "The tablature's creation date",
  })
  createdAt: Date;

  @ApiProperty({
    example: new Date(),
    description: "The tablature's last update date",
  })
  updatedAt: Date;

  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The tablature's creator's unique identifier",
  })
  createdById: string;

  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The tablature's last updater's unique identifier",
  })
  updatedById: string;
}
