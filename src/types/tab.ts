import { z } from "zod";

export const TabSchema = z.object({
  id: z.number().positive(),
  title: z.string(),
  content: z.string(),
  created: z.date(),
  modified: z.date(),
});

export type Tab = z.infer<typeof TabSchema>;
