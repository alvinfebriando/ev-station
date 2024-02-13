namespace astra_otoparts;

public record AddOrderRequest(int StationId, int Kwh);
public record UpdateProgressRequest(int Kwh);