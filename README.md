# Infinity Value

## Install

Choose one of the following installation methods:

>Note: Check the version after # in the GitHub URL for the latest changes listed in the Changelog.

### Install via Unity Package Manager (UPM)
1. Open UPM and click the + button in the top left. 
2. Select Install package from git URL....
3. Enter `https://github.com/achieveonepark/InfinityValue.git#1.0.0` and click Install.

### Manual Addition

Open the manifest.json file in your Unity project’s Packages folder.
Add the following line under dependencies:

```json
"com.achieve.quick-save": "https://github.com/achieveonepark/InfinityValue.git#1.0.0"
```

##  Description
- This package allows you to use custom units like A, B, C, etc., instead of standard units like million, billion, or trillion, making it easy to represent and work with extremely large numbers in a segmented format.
- If the Newtonsoft.Json package is installed, a JsonConverter is automatically registered to handle serialization seamlessly.
- Data is formatted in units such as "300F 200E" or "200AE 578AD," with operations performed directly on these representations. Calling ToString() will display the data in this format.
- Migration from standard primitive data types is supported, and all standard C# operators (comparison, arithmetic) are compatible.<br><br>

#### The following constructors are available:
```
- int
- long
- BigInteger
- string
- float
```

How to Use
```csharp
using Achieve.InfinityValue;
using System.Numerics;

public class A
{
    public InfinityValue A;
    public InfinityValue B;
    public InfinityValue C;
    public InfinityValue D;

    public A()
    {
        A = 1;                              // Can be initialized as an int
        B = "300F 200C";                    // Can be initialized as a formatted string
        C = 3.0f;                           // Supports float initialization
        D = new BigInteger(30000000000000); // Can handle BigInteger values

        var d = A + B;                      // Basic arithmetic: addition
        var e = B * C;                      // Basic arithmetic: multiplication
        var f = B / C;                      // Basic arithmetic: division

        Debug.Log(D.ToString()); // Outputs as "30D"
    }
}

```

