# Delta-Bed-Calibration-Tool

This tool is archived, refer to github.com/coelacant1/OpenDACT

A tool for calibrating the bed of delta-style 3D printers, achieving an accuracy of ±0.015 mm (or better) in under five minutes.

## Overview
This program corrects key parameters in a delta printer’s firmware (via EEPROM) to achieve a level, accurate print surface without physically adjusting endstops or shimming the bed. Tested from an initial ±2 mm error down to ±0.015 mm.

**Key parameters calibrated:**
- **Alpha Rotation**  
- **Delta Radius**  
- **XYZ Offset**  
- **Horizontal Radius**  
- **Diagonal Rod Length**

## Benefits

- **Tolerates Non-Square Towers:** It will still calibrate even if tower angles aren’t perfect.  
- **No Endstop Tweaks:** You won’t need to touch or adjust endstops.  
- **Warps & Slants:** Corrects for warped or slanted beds, including warped borosilicate plates.  
- **Minimal Physical Adjustments:** All changes occur in software (EEPROM), saving time and hassle.

## How It Works

1. **XYZ Offset**  
   - Identifies the most extreme tower offset and applies a compensation.  
   - Ensures all towers are brought closer to zero, enabling accurate diagonal rod calibration.  

2. **Horizontal Radius**  
   - Averages bed height at each tower (and “virtual” towers) to balance measurements around zero.  
   - Adjusts individual tower heights and the global horizontal radius parameter to flatten the bed.

3. **Alpha Rotation**  
   - Fine-tunes alignment of opposing (virtual) towers by rotating their reference angles (α).  
   - Balances the measured height on opposite towers to ensure uniformity.

4. **Diagonal Rod**  
   - Changing rod length shifts the height at each tower differently.  
   - This is counter-corrected by subsequent adjustments (e.g., horizontal radius) until all towers converge near zero.

By iterating these steps, you can theoretically achieve extremely tight tolerances—even if the bed is mechanically warped.

## Usage

1. **Input Current Settings:** Enter your existing EEPROM parameters into the program.  
2. **Run Calibration:** Click “Calibrate” to generate new, corrected values.  
3. **Update EEPROM:** Copy the new values back into your firmware EEPROM.  
4. **(Optional) Repeat:** For ultra-precise prints, run additional iterations.

## Future Plans

- Improve the HTML interface or create a desktop app.  
- Add carriage offset calibration.  
- Further refine accuracy and automation.

## Disclaimer

Use this program **at your own risk**. The author assumes no liability for any issues arising from its use.
