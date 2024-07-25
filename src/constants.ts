import { User, UserRole } from '@prisma/client';

export const JWT_SIGNING_KEY = 'REPLACE WITH ENV VAR';
export const REDACTED_STRING = 'REDACTED';

export const mockMemberUser: User = {
  id: 'mockIdMember',
  email: 'mockEmailMember',
  username: 'mockUsernameMember',
  passwordHash:
    '$argon2d$v=19$m=16,t=2,p=1$MXg5a1VoSGJkREszcks3Zw$I+8VewLVep2i68Op4EoJGA',
  roles: [UserRole.Member],
  createdAt: new Date(),
  updatedAt: new Date(),
};

export const mockUserPassword = 'mockPassword';

export const mockAdminUser: User = {
  id: 'mockIdAdmin',
  email: 'mockEmailAdmin',
  username: 'mockUsernameAdmin',
  passwordHash:
    '$argon2d$v=19$m=16,t=2,p=1$MXg5a1VoSGJkREszcks3Zw$I+8VewLVep2i68Op4EoJGA',
  roles: [UserRole.Member, UserRole.Admin],
  createdAt: new Date(),
  updatedAt: new Date(),
};
