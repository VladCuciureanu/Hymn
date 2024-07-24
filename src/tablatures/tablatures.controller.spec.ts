import { Test, TestingModule } from '@nestjs/testing';
import { TablaturesController } from './tablatures.controller';
import { TablaturesService } from './tablatures.service';
import { PrismaModule } from '../prisma/prisma.module';

describe('TablaturesController', () => {
  let controller: TablaturesController;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [TablaturesController],
      providers: [TablaturesService],
      imports: [PrismaModule],
    }).compile();

    controller = module.get<TablaturesController>(TablaturesController);
  });

  it('should be defined', () => {
    expect(controller).toBeDefined();
  });
});
