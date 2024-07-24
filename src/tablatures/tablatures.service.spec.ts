import { Test, TestingModule } from '@nestjs/testing';
import { TablaturesService } from './tablatures.service';
import { PrismaModule } from '../prisma/prisma.module';
import { PrismaService } from '../prisma/prisma.service';
import { mockAdminUser, mockMemberUser } from '../constants';
import { NotFoundException, UnauthorizedException } from '@nestjs/common';
import { getError } from '../utils/get-error';
import { mockDeep, DeepMockProxy } from 'jest-mock-extended';
import { TablatureEntity } from './entities/tablature.entity';

const mockTablature: TablatureEntity = {
  id: 'mockId',
  title: 'mockTitle',
};

describe('TablaturesService', () => {
  let service: TablaturesService;
  let db: DeepMockProxy<PrismaService>;

  beforeEach(async () => {
    const module: TestingModule = await Test.createTestingModule({
      providers: [TablaturesService],
      imports: [PrismaModule],
    })
      .overrideProvider(PrismaService)
      .useValue(mockDeep<PrismaService>())
      .compile();

    service = module.get<TablaturesService>(TablaturesService);
    db = module.get<DeepMockProxy<PrismaService>>(PrismaService);
  });

  it('should create a tablature if admin', async () => {
    jest.spyOn(db.tablature, 'create').mockResolvedValueOnce(mockTablature);

    const tablature = await service.create({
      dto: { title: mockTablature.title },
      accountability: mockAdminUser,
    });

    expect(tablature).toEqual(mockTablature);
  });

  it("shouldn't create a tablature if not admin", async () => {
    jest.spyOn(db.tablature, 'create').mockResolvedValueOnce(mockTablature);

    const error = await getError(
      async () =>
        await service.create({
          dto: { title: mockTablature.title },
          accountability: mockMemberUser,
        }),
    );

    expect(error).toBeInstanceOf(UnauthorizedException);
  });

  it('should return an array of tablatures', async () => {
    jest.spyOn(db.tablature, 'findMany').mockResolvedValueOnce([mockTablature]);

    const tablatures = await service.findAll();

    expect(tablatures).toEqual([mockTablature]);
  });

  it('should return a tablature', async () => {
    jest.spyOn(db.tablature, 'findUnique').mockResolvedValueOnce(mockTablature);

    const tablature = await service.findOne({
      id: mockTablature.id,
    });

    expect(tablature).toEqual(mockTablature);
  });

  it("shouldn't return a non-existent tablature", async () => {
    jest.spyOn(db.tablature, 'findUnique').mockResolvedValueOnce(null);

    const error = await getError(async () => {
      await service.findOne({
        id: mockTablature.id,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });

  it('should update tablature if admin', async () => {
    jest.spyOn(db.tablature, 'update').mockResolvedValueOnce({
      ...mockTablature,
      title: mockTablature.title + '1',
    });

    const tablature = await service.update({
      id: mockTablature.id,
      dto: {
        title: mockTablature.title + '1',
      },
      accountability: mockAdminUser,
    });

    expect(tablature).toEqual({
      ...mockTablature,
      title: mockTablature.title + '1',
    });
  });

  it("shouldn't update tablature if not admin", async () => {
    jest.spyOn(db.tablature, 'update').mockResolvedValueOnce(mockTablature);

    const error = await getError(async () => {
      await service.update({
        id: mockTablature.id,
        dto: { title: mockTablature.title + '1' },
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(UnauthorizedException);
  });

  it("shouldn't update non-existent tablatures", async () => {
    jest.spyOn(db.tablature, 'update').withImplementation(
      () => null as any,
      () => {},
    );

    const error = await getError(async () => {
      await service.update({
        id: mockTablature.id,
        dto: { title: mockTablature.title + '1' },
        accountability: mockAdminUser,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });

  it('should delete tablature if admin', async () => {
    jest.spyOn(db.tablature, 'delete').mockResolvedValueOnce(mockTablature);

    const tablature = await service.remove({
      id: mockTablature.id,
      accountability: mockAdminUser,
    });

    expect(tablature).toEqual(mockTablature);
  });

  it("shouldn't delete tablature if not admin", async () => {
    jest.spyOn(db.tablature, 'delete').mockResolvedValueOnce(mockTablature);

    const error = await getError(async () => {
      await service.remove({
        id: mockTablature.id,
        accountability: mockMemberUser,
      });
    });

    expect(error).toBeInstanceOf(UnauthorizedException);
  });

  it("shouldn't delete non-existent tablatures", async () => {
    jest.spyOn(db.tablature, 'delete').withImplementation(
      () => null as any,
      () => {},
    );

    const error = await getError(async () => {
      await service.remove({
        id: mockTablature.id,
        accountability: mockAdminUser,
      });
    });

    expect(error).toBeInstanceOf(NotFoundException);
  });
});
