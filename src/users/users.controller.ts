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
import { UsersService } from './users.service';
import { CreateUserDto } from './dto/create-user.dto';
import { UpdateUserDto } from './dto/update-user.dto';
import {
  ApiBadRequestResponse,
  ApiCreatedResponse,
  ApiNotFoundResponse,
  ApiOkResponse,
  ApiOperation,
  ApiTags,
  ApiUnauthorizedResponse,
} from '@nestjs/swagger';
import { UserEntity } from './entities/user.entity';
import { PrismaClientExceptionFilter } from '../prisma/exceptions/prisma-client-exception.filter';
import { Public } from '../auth/decorators/public.decorator';
import { AuthenticatedRequest } from 'src/auth/interfaces/authenticated-request.interface';

@ApiTags('users')
@Controller('users')
@UseFilters(PrismaClientExceptionFilter)
export class UsersController {
  constructor(private readonly usersService: UsersService) {}

  @Post()
  @Public()
  @ApiOperation({ summary: 'Create user' })
  @ApiCreatedResponse({ type: UserEntity })
  @ApiBadRequestResponse()
  create(@Body() createUserDto: CreateUserDto) {
    return this.usersService.create(createUserDto);
  }

  @Get()
  @Public()
  @ApiOperation({ summary: 'Find multiple users' })
  @ApiOkResponse({ type: [UserEntity] })
  findAll(@Req() req: AuthenticatedRequest) {
    return this.usersService.findAll({ accountability: req.user });
  }

  @Get(':id')
  @Public()
  @ApiOperation({ summary: 'Find user by id' })
  @ApiOkResponse({ type: UserEntity })
  @ApiNotFoundResponse()
  findOne(@Req() req: AuthenticatedRequest, @Param('id') id: string) {
    return this.usersService.findOne({ id, accountability: req.user });
  }

  @Patch(':id')
  @ApiOperation({ summary: 'Update user' })
  @ApiOkResponse({ type: UserEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  @ApiBadRequestResponse()
  update(
    @Req() req: AuthenticatedRequest,
    @Param('id') id: string,
    @Body() updateUserDto: UpdateUserDto,
  ) {
    return this.usersService.update({
      id,
      dto: updateUserDto,
      accountability: req.user,
    });
  }

  @Delete(':id')
  @ApiOperation({ summary: 'Delete user' })
  @ApiOkResponse({ type: UserEntity })
  @ApiNotFoundResponse()
  @ApiUnauthorizedResponse()
  remove(@Req() req: AuthenticatedRequest, @Param('id') id: string) {
    return this.usersService.remove({ id, accountability: req.user });
  }
}
