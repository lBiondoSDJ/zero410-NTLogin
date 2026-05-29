# zero410-NTLogin

Fork proprietario di [DeepSignalTech/NinjaTraderAutoLogin V1.0](https://github.com/DeepSignalTech/NinjaTraderAutoLogin/releases/tag/V1.0) con fix per ambienti high-DPI.

Parte della pipeline **NT Auto Export** documentata in `TradingHub/scripts/nt-auto-export/NT_AUTO_EXPORT_PIPELINE.md`.

## Differenza dall'upstream

L'upstream V1.0 inserisce il focus nel campo password tramite click a coordinate fisse (`Left+30, Top+210` rispetto al top-left della finestra). Questo approccio fallisce su display ad alto DPI scaling (es. notebook con scaling 150%): le coordinate non vengono riscalate, il click cade fuori dal campo password, l'auto-login non riesce.

**Fix (D13):** sostituito il click a coordinate con sequenza di 3 `SendKeys.SendWait("{TAB}")` per portare il focus nel campo password. Approccio DPI-independent, multi-monitor-safe, indipendente dalla posizione e dimensione della finestra. Ordine TAB verificato empiricamente su NinjaTrader 8.

## Modifiche al sorgente upstream

Unica modifica: `NTLogin/Program.cs` nella sezione di iniezione password. Tracciata con commento `zero410 fix (D13)` nel codice.

Nessun altro file modificato. La logica originale di avvio NinjaTrader, polling del processo, gestione clipboard e invio Enter resta invariata.

## Uso

Identico all'upstream:
NTLogin.exe "LOGIN_NAME" "PASSWORD"

oppure con path NT custom:
NTLogin.exe "LOGIN_NAME" "PASSWORD" "C:\Program Files\NinjaTrader 8\bin\NinjaTrader.exe"

Vedi `README.md` originale di upstream per dettagli sull'uso e creazione shortcut Windows.

## Licenza

Mantenuta MIT (eredita da upstream). Vedi `LICENSE`.

## Riferimenti

- Upstream: https://github.com/DeepSignalTech/NinjaTraderAutoLogin
- Pipeline documentation: `TradingHub/scripts/nt-auto-export/NT_AUTO_EXPORT_PIPELINE.md` §4.2
- Decisione D12 (fork proprietario), D13 (fix TAB-based): vedi pipeline doc §6