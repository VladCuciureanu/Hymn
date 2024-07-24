import { User, UserRole } from '@prisma/client';

export const JWT_SIGNING_KEY = 'REPLACE WITH ENV VAR';
export const REDACTED_STRING = 'REDACTED';

export const mockMemberUser: User = {
  id: 'mockIdMember',
  email: 'mockEmailMember',
  username: 'mockUsernameMember',
  passwordHash: 'mockPasswordHash',
  roles: [UserRole.Member],
};

export const mockAdminUser: User = {
  id: 'mockIdAdmin',
  email: 'mockEmailAdmin',
  username: 'mockUsernameAdmin',
  passwordHash: 'mockPasswordHash',
  roles: [UserRole.Member, UserRole.Admin],
};
