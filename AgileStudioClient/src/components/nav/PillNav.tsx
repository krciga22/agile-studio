import './PillNav.css';

type Props = {
  pillNavItems: PillNavItem[],
  className?: string
}

type OnSelectProps = {
  navItem: PillNavItem
};

export type PillNavItem = {
  label: string,
  onSelect: (props: OnSelectProps) => void
  key?: string,
  selected?: boolean,
};

export default function PillNav({ pillNavItems, className }: Props) {
  return (
    <div className={"pill-nav d-flex " + className}>
      { pillNavItems.map((navItem) => {
        const selectedClass = (navItem.selected === true) ? 'selected' : '';

        const onSelectProps:OnSelectProps = {
          navItem: navItem
        };

        return <div className={["pill-nav-item p-3", selectedClass].join(" ")}
                    key={navItem?.key ?? navItem.label}
                    onClick={() => {navItem.onSelect(onSelectProps)}}>{navItem.label}</div>;
      }) }
    </div>
  );
}
