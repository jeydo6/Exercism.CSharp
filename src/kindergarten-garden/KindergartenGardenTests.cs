using Xunit;

public class KindergartenGardenTests
{
    [Fact]
    public void Partial_garden_garden_with_single_student()
    {
        var sut = new KindergartenGarden("RC\nGG");
        Assert.Equal(new[] { Plant.Radishes, Plant.Clover, Plant.Grass, Plant.Grass }, sut.Plants("Alice"));
    }

    [Fact]
    public void Partial_garden_different_garden_with_single_student()
    {
        var sut = new KindergartenGarden("VC\nRC");
        Assert.Equal(new[] { Plant.Violets, Plant.Clover, Plant.Radishes, Plant.Clover }, sut.Plants("Alice"));
    }

    [Fact]
    public void Partial_garden_garden_with_two_students()
    {
        var sut = new KindergartenGarden("VVCG\nVVRC");
        Assert.Equal(new[] { Plant.Clover, Plant.Grass, Plant.Radishes, Plant.Clover }, sut.Plants("Bob"));
    }

    [Fact]
    public void Partial_garden_multiple_students_for_the_same_garden_with_three_students_second_students_garden()
    {
        var sut = new KindergartenGarden("VVCCGG\nVVCCGG");
        Assert.Equal(new[] { Plant.Clover, Plant.Clover, Plant.Clover, Plant.Clover }, sut.Plants("Bob"));
    }

    [Fact]
    public void Partial_garden_multiple_students_for_the_same_garden_with_three_students_third_students_garden()
    {
        var sut = new KindergartenGarden("VVCCGG\nVVCCGG");
        Assert.Equal(new[] { Plant.Grass, Plant.Grass, Plant.Grass, Plant.Grass }, sut.Plants("Charlie"));
    }

    [Fact]
    public void Full_garden_for_alice_first_students_garden()
    {
        var sut = new KindergartenGarden("VRCGVVRVCGGCCGVRGCVCGCGV\nVRCCCGCRRGVCGCRVVCVGCGCV");
        Assert.Equal(new[] { Plant.Violets, Plant.Radishes, Plant.Violets, Plant.Radishes }, sut.Plants("Alice"));
    }

    [Fact]
    public void Full_garden_for_bob_second_students_garden()
    {
        var sut = new KindergartenGarden("VRCGVVRVCGGCCGVRGCVCGCGV\nVRCCCGCRRGVCGCRVVCVGCGCV");
        Assert.Equal(new[] { Plant.Clover, Plant.Grass, Plant.Clover, Plant.Clover }, sut.Plants("Bob"));
    }

    [Fact]
    public void Full_garden_for_kincaid_second_to_last_students_garden()
    {
        var sut = new KindergartenGarden("VRCGVVRVCGGCCGVRGCVCGCGV\nVRCCCGCRRGVCGCRVVCVGCGCV");
        Assert.Equal(new[] { Plant.Grass, Plant.Clover, Plant.Clover, Plant.Grass }, sut.Plants("Kincaid"));
    }

    [Fact]
    public void Full_garden_for_larry_last_students_garden()
    {
        var sut = new KindergartenGarden("VRCGVVRVCGGCCGVRGCVCGCGV\nVRCCCGCRRGVCGCRVVCVGCGCV");
        Assert.Equal(new[] { Plant.Grass, Plant.Violets, Plant.Clover, Plant.Violets }, sut.Plants("Larry"));
    }
}
