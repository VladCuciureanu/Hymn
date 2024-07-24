import { ApiProperty } from '@nestjs/swagger';
import { Tablature } from '@prisma/client';
import { IsAlphanumeric, IsString, MinLength } from 'class-validator';

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
}
