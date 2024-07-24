import { Test, TestingModule } from '@nestjs/testing';
import { TablaturesService } from './tablatures.service';
import { PrismaModule } from '../prisma/prisma.module';

describe('TablaturesService', () => {
  let service: TablaturesService;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [TablaturesService],
      imports: [PrismaModule],
    }).compile();

    service = module.get<TablaturesService>(TablaturesService);
  });

  it('should be defined', () => {
    expect(service).toBeDefined();
  });
});
