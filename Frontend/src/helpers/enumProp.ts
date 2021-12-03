// Если нужно как тип prop задать enum
// Пример: @Prop(EnumProp(WorkspaceDisplay.Both, WorkspaceDisplay)) readonly display;
export const EnumProp = (d: string | number, e: Record<any, any>) => {
  return {
    default: d,
    validator: (v: any) => e[v] !== undefined,
  };
};
