export interface IOrgChartNode {
    id: number;
    firstName: string;
    lastName: string;
    nodes: IOrgChartNode[];
}