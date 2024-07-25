import { Module } from '@nestjs/common';
import { TablaturesService } from './tablatures.service';
import { TablaturesController } from './tablatures.controller';
import { PrismaModule } from '../prisma/prisma.module';

@Module({
  controllers: [TablaturesController],
  providers: [TablaturesService],
  imports: [PrismaModule],
  exports: [TablaturesService],
})
export class TablaturesModule {}
