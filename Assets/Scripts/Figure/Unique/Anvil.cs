using UnityEngine;

public class Anvil : FigureBase
{
	public Anvil()
	{
		shapeType = ShapeType.Anvil;
	}

	public override string Key => shapeType.ToString();
}
