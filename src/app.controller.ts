import { Controller, Get } from '@nestjs/common';
import { Public } from './auth/decorators/public.decorator';
import { ApiOkResponse } from '@nestjs/swagger';

@Controller()
export class AppController {
  @Get('health')
  @Public()
  @ApiOkResponse({ description: 'API is healthy' })
  getHello() {}
}
