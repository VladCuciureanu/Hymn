import { Body, Controller, Get, Post, Req, Res } from '@nestjs/common';
import { LoginDto } from './dto/login.dto';
import { AuthService } from './auth.service';
import {
  ApiBadRequestResponse,
  ApiBearerAuth,
  ApiOkResponse,
  ApiTags,
  ApiUnauthorizedResponse,
} from '@nestjs/swagger';
import { Public } from './decorators/public.decorator';
import { Request, Response } from 'express';
import { RegisterDto } from './dto/register.dto';

@ApiTags('auth')
@Controller('auth')
export class AuthController {
  constructor(private authService: AuthService) {}

  @Post('login')
  @Public()
  @ApiOkResponse({ description: 'User logged in' })
  @ApiUnauthorizedResponse({ description: 'Invalid credentials' })
  @ApiBadRequestResponse({ description: 'Invalid request' })
  async login(
    @Body() loginDto: LoginDto,
    @Res({ passthrough: true }) response: Response,
  ) {
    const payload = await this.authService.login(loginDto);

    response.cookie('token', payload.access_token);

    return payload;
  }

  @Post('register')
  @Public()
  @ApiOkResponse({ description: 'User logged in' })
  @ApiUnauthorizedResponse({ description: 'Invalid credentials' })
  @ApiBadRequestResponse({ description: 'Invalid request' })
  async register(
    @Body() loginDto: RegisterDto,
    @Res({ passthrough: true }) response: Response,
  ) {
    const payload = await this.authService.register(loginDto);

    response.cookie('token', payload.access_token);

    return payload;
  }

  @Get('profile')
  @ApiBearerAuth()
  @ApiOkResponse({ description: 'User profile' })
  @ApiUnauthorizedResponse({ description: 'Invalid credentials' })
  async profile(@Req() request: Request) {
    return request.user;
  }
}
