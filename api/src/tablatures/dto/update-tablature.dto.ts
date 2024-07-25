import { PartialType } from '@nestjs/mapped-types';
import { CreateTablatureDto } from './create-tablature.dto';

export class UpdateTablatureDto extends PartialType(CreateTablatureDto) {}
