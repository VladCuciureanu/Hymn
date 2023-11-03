"use client";
import { ColumnDef } from "@tanstack/react-table";
import { TabsTableColumnHeader } from "./column-header";
import { TabsTableRowActions } from "./row-actions";
import { Tab } from "@/types/tab";

export const TabsTableColumns: ColumnDef<Tab>[] = [
  {
    accessorKey: "id",
    header: ({ column }) => <TabsTableColumnHeader column={column} title="#" />,
    cell: ({ row }) => <div className="w-[80px]">{row.getValue("id")}</div>,
    enableSorting: true,
    enableHiding: true,
  },
  {
    accessorKey: "title",
    header: ({ column }) => (
      <TabsTableColumnHeader column={column} title="Title" />
    ),
    cell: ({ row }) => {
      return (
        <div className="flex space-x-2">
          <span className="max-w-[500px] truncate font-medium">
            {row.getValue("title")}
          </span>
        </div>
      );
    },
  },
  {
    id: "actions",
    cell: ({ row }) => <TabsTableRowActions row={row} />,
  },
];
