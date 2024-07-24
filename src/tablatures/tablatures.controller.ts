import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  UseFilters,
} from '@nestjs/common';
import { TablaturesService } from './tablatures.service';
import { CreateTablatureDto } from './dto/create-tablature.dto';
import { UpdateTablatureDto } from './dto/update-tablature.dto';
import {
  ApiBadRequestResponse,
  ApiCreatedResponse,
  ApiNotFoundResponse,
  ApiOkResponse,
  ApiOperation,
  ApiTags,
  ApiUnauthorizedResponse,
} from '@nestjs/swagger';
import { TablatureEntity } from './entities/tablature.entity';
import { PrismaClientExceptionFilter } from '../prisma/exceptions/prisma-client-exception.filter';
import { Public } from '../auth/decorators/public.decorator';

@ApiTags('tablatures')
@Controller('tablatures')
@UseFilters(PrismaClientExceptionFilter)
export class TablaturesController {
  constructor(private readonly tablaturesService: TablaturesService) {}

  @Post()
  @ApiOperation({ summary: 'Create tablature' })
  @ApiCreatedResponse({ type: TablatureEntity })
  @ApiBadRequestResponse()
  create(@Body() createTablatureDto: CreateTablatureDto) {
    return this.tablaturesService.create(createTablatureDto);
  }

  @Get()
  @Public()
  @ApiOperation({ summary: 'Find multiple tablatures' })
  @ApiOkResponse({ type: [TablatureEntity] })
  findAll() {
    return this.tablaturesService.findAll();
  }

  @Get(':id')
  @Public()
  @ApiOperation({ summary: 'Find tablature by id' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  findOne(@Param('id') id: string) {
    return this.tablaturesService.findOne(id);
  }

  @Patch(':id')
  @ApiOperation({ summary: 'Update tablature' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  @ApiBadRequestResponse()
  update(
    @Param('id') id: string,
    @Body() updateTablatureDto: UpdateTablatureDto,
  ) {
    return this.tablaturesService.update(id, updateTablatureDto);
  }

  @Delete(':id')
  @ApiOperation({ summary: 'Delete tablature' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  remove(@Param('id') id: string) {
    return this.tablaturesService.remove(id);
  }
}
