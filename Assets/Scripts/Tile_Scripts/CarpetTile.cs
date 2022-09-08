public class CarpetTile : Tile
{
    private int _lightValue = 0;
    public override int LightValue => _lightValue;

    private int _soundValue = 0;
    public override int SoundValue => _soundValue;
    
    public override void UpdateLightValue(int amount)
    {
        _lightValue += amount;
    }

    public override void UpdateSoundValue(int amount)
    {
        _soundValue += amount;
    }
}
