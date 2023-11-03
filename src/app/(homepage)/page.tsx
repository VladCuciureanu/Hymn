import { TabsTable } from "@/components/homepage/tabs-table";
import { Tab } from "@/types/tab";

export default function Home() {
  const tabs: Tab[] = [
    {
      id: 1,
      title: "Test Tab 1",
      content: "",
      created: new Date(),
      modified: new Date(),
    },
    {
      id: 2,
      title: "Test Tab 2",
      content: "",
      created: new Date(),
      modified: new Date(),
    },
    {
      id: 3,
      title: "Test Tab 3",
      content: "",
      created: new Date(),
      modified: new Date(),
    },
    {
      id: 4,
      title: "Test Tab 4",
      content: "",
      created: new Date(),
      modified: new Date(),
    },
  ];

  return (
    <div className="hidden h-full flex-1 flex-col space-y-8 p-8 md:flex">
      <div className="flex items-center justify-between space-y-2">
        <div>
          <h2 className="text-2xl font-bold tracking-tight">Welcome back!</h2>
          <p className="text-muted-foreground">
            Here&apos;s a list of your tabs to explore!
          </p>
        </div>
        <div className="flex items-center space-x-2">{/* <UserNav /> */}</div>
      </div>
      <TabsTable data={tabs} />
    </div>
  );
}
