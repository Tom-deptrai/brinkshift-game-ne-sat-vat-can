# Brinkshift

**Brinkshift** là dự án game mobile thương mại cho iOS và Android.

Repo: `brinkshift-game-ne-sat-vat-can`
Unity project folder: `Brinkshift/`

## Ý tưởng cốt lõi hiện tại

Brinkshift là game arcade 2D/2.5D portrait, điều khiển bằng một ngón tay. Người chơi di chuyển nhân vật bằng **relative drag** để né các hazard. Điểm số chủ yếu đến từ **Graze**: lướt càng sát hazard mà không va chạm thì càng được thưởng nhiều.

Gameplay hiện tại tập trung vào:
- Analog one-finger control.
- Risk/reward rõ ràng: chơi an toàn được ít điểm, chơi sát nguy hiểm được nhiều điểm.
- Chain Meter / multiplier tăng khi graze liên tục và giảm khi ngừng graze.
- Restart gần như tức thì.
- Procedural sequencing bằng các chunk được thiết kế sẵn.
- Game feel rõ: feedback hình ảnh, haptic, SFX và micro slow-motion cho các pha graze đặc biệt sát.

## Trạng thái dự án

**Giai đoạn hiện tại: Gameplay Validation Prototype.**

Mục tiêu chưa phải làm game hoàn chỉnh. Mục tiêu là chứng minh:
1. Điều khiển cảm ứng có tốt không.
2. Graze có thực sự vui không.
3. Người chơi có chủ động mạo hiểm để kiếm điểm không.
4. Người chơi có muốn chơi lại sau khi chết không.
5. Gameplay có đủ hấp dẫn khi xem video ngắn không.

## Công nghệ hiện tại

- Engine: Unity 6.5 (6000.5.10f1), Apple Silicon
- Rendering: Unity 2D / URP 2D Renderer
- Orientation: Portrait
- Platforms: iOS + Android + Web build support
- Prototype: không third-party SDK

## Không làm trong prototype đầu tiên

Không Ads, IAP, Firebase, analytics, backend, leaderboard, shop, skins, daily missions hoặc art hoàn chỉnh trước khi gameplay core vượt validation gate.

## Tài liệu

- [`PROJECT_SPEC.md`](PROJECT_SPEC.md) — trạng thái thiết kế hiện tại của game.
- [`ROADMAP.md`](ROADMAP.md) — roadmap linh hoạt hiện tại.
- [`AI_CODING_RULES.md`](AI_CODING_RULES.md) — quy tắc cho AI coding.
- [`PLAYTEST_CHECKLIST.md`](PLAYTEST_CHECKLIST.md) — checklist nghiệm thu prototype trên thiết bị thật.

## Nguyên tắc dự án

Gameplay phải vui trước. Spec, roadmap, monetization và công nghệ có thể thay đổi nếu playtest hoặc dữ liệu chứng minh có phương án tốt hơn.
