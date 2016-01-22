namespace WHOISClientWebApp {
    export interface WhoisResponse {
        RespondedServers: string[];
        Raw: string;
        OrganizationName: string;
        AddressRange: { Begin: string; End: string };
    }
}