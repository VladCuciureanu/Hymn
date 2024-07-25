import { Test, TestingModule } from '@nestjs/testing';
import { TablaturesController } from './tablatures.controller';
import { TablaturesService } from './tablatures.service';
import { PrismaModule } from '../prisma/prisma.module';
import { TablatureEntity } from './entities/tablature.entity';
import { AuthenticatedRequest } from '../auth/interfaces/authenticated-request.interface';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { CreateTablatureDto } from './dto/create-tablature.dto';
import { mockMemberUser } from '../constants';
import { TablatureStatus } from '@prisma/client';

const mockTablature: TablatureEntity = {
  id: 'mockId',
  title: 'mockEmail',
  status: TablatureStatus.Draft,
  createdAt: new Date(),
  updatedAt: new Date(),
  createdById: '',
  updatedById: '',
};

const mockAuthenticatedRequest: AuthenticatedRequest = {
  user: mockMemberUser,
} as any;

describe('TablaturesController', () => {
  let controller: TablaturesController;
  let service: DeepMockProxy<TablaturesService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      controllers: [TablaturesController],
      providers: [TablaturesService],
      imports: [PrismaModule],
    })
      .overrideProvider(TablaturesService)
      .useValue(mockDeep(TablaturesService))
      .compile();

    controller = module.get<TablaturesController>(TablaturesController);
    service = module.get<DeepMockProxy<TablaturesService>>(TablaturesService);
  });

  describe('create', () => {
    it('should create a tablature', async () => {
      jest.spyOn(service, 'create').mockResolvedValueOnce(mockTablature);

      const dto: CreateTablatureDto = {
        title: mockTablature.title,
      };

      const tablature = await controller.create(mockAuthenticatedRequest, dto);

      expect(service.create).toHaveBeenCalled();
      expect(tablature).toBeDefined();
    });
  });

  describe('findAll', () => {
    it('should return an array of tablatures', async () => {
      jest.spyOn(service, 'findAll').mockResolvedValueOnce([mockTablature]);

      const tablatures = await controller.findAll(mockAuthenticatedRequest);

      expect(service.findAll).toHaveBeenCalled();
      expect(tablatures).toEqual([mockTablature]);
    });
  });

  describe('findOne', () => {
    it('should return a tablature', async () => {
      jest.spyOn(service, 'findOne').mockResolvedValueOnce(mockTablature);

      const tablature = await controller.findOne(
        mockAuthenticatedRequest,
        mockTablature.id,
      );

      expect(service.findOne).toHaveBeenCalled();
      expect(tablature).toEqual(mockTablature);
    });
  });

  describe('update', () => {
    it('should update a tablature', async () => {
      const updatedTablature = { ...mockTablature, title: 'newTitle' };
      jest.spyOn(service, 'update').mockResolvedValueOnce(updatedTablature);

      const tablature = await controller.update(
        mockAuthenticatedRequest,
        mockTablature.id,
        {
          title: updatedTablature.title,
        },
      );

      expect(service.update).toHaveBeenCalled();
      expect(tablature).toEqual(updatedTablature);
    });
  });

  describe('remove', () => {
    it('should remove a tablature', async () => {
      jest.spyOn(service, 'remove').mockResolvedValueOnce(mockTablature);

      const tablature = await controller.remove(
        mockAuthenticatedRequest,
        mockTablature.id,
      );

      expect(service.remove).toHaveBeenCalled();
      expect(tablature).toEqual(mockTablature);
    });
  });
});
