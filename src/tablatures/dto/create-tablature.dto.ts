import { PickType } from '@nestjs/swagger';
import { TablatureEntity } from '../entities/tablature.entity';

export class CreateTablatureDto extends PickType(TablatureEntity, ['title']) {}
