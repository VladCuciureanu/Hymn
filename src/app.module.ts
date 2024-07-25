import { Module } from '@nestjs/common';
import { AppController } from './app.controller';
import { UsersModule } from './users/users.module';
import { PrismaModule } from './prisma/prisma.module';
import { AuthModule } from './auth/auth.module';
import { JwtAuthGuard } from './auth/guards/jwt-auth.guard';
import { APP_GUARD, Reflector } from '@nestjs/core';
import { TablaturesModule } from './tablatures/tablatures.module';

@Module({
  imports: [AuthModule, PrismaModule, TablaturesModule, UsersModule],
  controllers: [AppController],
  providers: [
    {
      provide: APP_GUARD,
      useFactory: (ref) => new JwtAuthGuard(ref),
      inject: [Reflector],
    },
  ],
})
export class AppModule {}
