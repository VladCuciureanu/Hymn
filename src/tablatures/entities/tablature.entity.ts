import { ApiProperty } from '@nestjs/swagger';
import { Tablature } from '@prisma/client';
import { IsString } from 'class-validator';

export class TablatureEntity implements Tablature {
  @ApiProperty({
    example: 'clyx5in0z0000128km7qeprls',
    description: "The tablature's unique identifier",
  })
  id: string;

  @ApiProperty({
    description: "The tablature's title",
  })
  @IsString()
  title: string;
}
