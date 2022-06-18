internal class SpaceAge
{
    private readonly int _seconds;

    public SpaceAge(int seconds) => _seconds = seconds;

    public double OnEarth() => _seconds / 31557600.0;

    public double OnMercury() => _seconds / 7600543.82;

    public double OnVenus() => _seconds / 19414149.05;

    public double OnMars() => _seconds / 59354032.69;

    public double OnJupiter() => _seconds / 374355659.1;

    public double OnSaturn() => _seconds / 929292362.9;

    public double OnUranus() => _seconds / 2651370019.0;

    public double OnNeptune() => _seconds / 5200418560.0;
}
