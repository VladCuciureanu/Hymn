import {
  Controller,
  Get,
  Post,
  Body,
  Patch,
  Param,
  Delete,
  UseFilters,
  Req,
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
import { AuthenticatedRequest } from 'src/auth/interfaces/authenticated-request.interface';

@ApiTags('tablatures')
@Controller('tablatures')
@UseFilters(PrismaClientExceptionFilter)
export class TablaturesController {
  constructor(private readonly tablaturesService: TablaturesService) {}

  @Post()
  @ApiOperation({ summary: 'Create tablature' })
  @ApiCreatedResponse({ type: TablatureEntity })
  @ApiBadRequestResponse()
  create(
    @Req() req: AuthenticatedRequest,
    @Body() createTablatureDto: CreateTablatureDto,
  ) {
    return this.tablaturesService.create({
      dto: createTablatureDto,
      accountability: req.user!,
    });
  }

  @Get()
  @Public()
  @ApiOperation({ summary: 'Find multiple tablatures' })
  @ApiOkResponse({ type: [TablatureEntity] })
  findAll(@Req() req: AuthenticatedRequest) {
    return this.tablaturesService.findAll({ accountability: req.user });
  }

  @Get(':id')
  @Public()
  @ApiOperation({ summary: 'Find tablature by id' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  findOne(@Req() req: AuthenticatedRequest, @Param('id') id: string) {
    return this.tablaturesService.findOne({
      id,
      accountability: req.user,
    });
  }

  @Patch(':id')
  @ApiOperation({ summary: 'Update tablature' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  @ApiBadRequestResponse()
  update(
    @Req() req: AuthenticatedRequest,
    @Param('id') id: string,
    @Body() updateTablatureDto: UpdateTablatureDto,
  ) {
    return this.tablaturesService.update({
      id,
      dto: updateTablatureDto,
      accountability: req.user!,
    });
  }

  @Delete(':id')
  @ApiOperation({ summary: 'Delete tablature' })
  @ApiOkResponse({ type: TablatureEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  remove(@Req() req: AuthenticatedRequest, @Param('id') id: string) {
    return this.tablaturesService.remove({ id, accountability: req.user! });
  }
}
