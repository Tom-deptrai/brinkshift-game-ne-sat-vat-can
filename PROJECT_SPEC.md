# PROJECT_SPEC.md — Brinkshift

> Living document. Tài liệu này phản ánh trạng thái hiện tại của dự án và có thể thay đổi sau playtest, dữ liệu hoặc quyết định mới.

## 1. Product Goal

Brinkshift là game mobile thương mại phát hành global trên Apple App Store và Google Play. Mục tiêu là tạo gameplay dễ hiểu, một tay, session ngắn, có skill ceiling cao, đủ khác biệt để tránh cảm giác clone/generic, và có khả năng phát triển bằng AI coding với chi phí vận hành thấp.

## 2. Core Concept hiện tại

Người chơi điều khiển một sinh vật nhỏ trong màn hình portrait bằng **relative drag**. Hazard liên tục di chuyển qua màn hình. Chạm hazard = chết.

Điểm số không đến chủ yếu từ việc sống lâu, mà từ **Graze**: lướt rất sát hazard nhưng không va chạm.

Triết lý core:
- Chơi an toàn → sống lâu nhưng điểm thấp.
- Chủ động lướt sát nguy hiểm → điểm và multiplier tăng nhanh.
- Skill nằm ở khả năng đọc quỹ đạo, giữ khoảng cách rất nhỏ và graze liên tục.
- Người chơi nên cảm thấy: “mình chết vì liều quá / xử lý sai”, không phải “game ăn gian”.

## 3. Controls

**Relative Drag**:
- Chạm ở vùng thuận tiện trên màn hình.
- Player di chuyển theo delta chuyển động của ngón tay, không teleport tới vị trí ngón tay.
- Nhấc tay và chạm lại không làm player nhảy vị trí.
- Player không nằm trực tiếp dưới đầu ngón tay để tránh che khuất hitbox.
- Sensitivity phải configurable và được tune trên máy thật.

Không dùng virtual joystick trong prototype.

## 4. Collision & Graze Model

Mỗi hazard có tối thiểu hai vùng:
- **Hit zone**: player chạm vào → chết.
- **Graze zone**: player đi vào vùng này mà không chạm hit zone → đủ điều kiện tạo graze.

Nguyên tắc hiện tại:
- Một hazard chỉ thưởng graze một lần cho mỗi lần đi qua hợp lệ.
- Graze cần tránh exploit kiểu đứng cạnh một hazard chậm để farm điểm.
- Điểm graze nên phụ thuộc chủ yếu vào **độ sát nguy hiểm** và **nhịp graze liên tục**.
- Các giá trị radius, tolerance, timing chưa khóa; phải expose thành tuning parameters.

## 5. Chain Meter

Không có manual cash-out.

Graze thành công sẽ tăng **Chain Meter**.
- Graze liên tục → meter duy trì/tăng.
- Không graze trong một khoảng thời gian → meter decay.
- Multiplier được suy ra từ meter.
- Điểm cộng tự động, không có “điểm chưa bank”.

Mục tiêu của hệ thống này là chống lối chơi quá an toàn và khuyến khích người chơi tiếp tục ở gần nguy hiểm mà không thêm nút hay khái niệm phức tạp.

## 6. Thread / Visual Feedback

Thread không còn là mechanic gameplay bắt buộc.

Có thể dùng thread/trail/glow như **visual feedback** nếu không làm rối màn hình. Hazard phải luôn có độ ưu tiên thị giác cao hơn mọi VFX trang trí.

## 7. Core Loop

Start run
→ relative drag
→ đọc hazard
→ chọn khoảng cách an toàn hoặc graze sát
→ graze tăng score + Chain Meter
→ multiplier tăng nếu duy trì chuỗi
→ difficulty tăng theo thời gian
→ va chạm
→ game over
→ restart gần như tức thì.

## 8. Prototype Obstacles

Prototype chỉ cần 2 archetype ban đầu:
1. **Straight Drifter** — hazard đi theo quỹ đạo đơn giản, tốc độ/kích thước thay đổi.
2. **Lateral Drifter** — vừa tiến theo hướng chính vừa có chuyển động ngang/chéo.

Không thêm rotating/pulse/twin mechanics trước khi hai loại cơ bản chứng minh Graze đã vui.

## 9. Procedural Generation

Dùng **hand-authored chunks + sequencer**, không random từng hazard hoàn toàn.

Mỗi chunk có thể chứa:
- difficulty weight;
- tag như Teach / Rhythm / Risk / Pressure / Breather;
- entry state;
- exit state;
- safety constraints.

Sequencer chỉ ghép các chunk tương thích.

Không tăng speed vô hạn. Sau một mức hợp lý, độ khó ưu tiên tăng bằng:
- density;
- spacing;
- trajectory overlap;
- thời gian không có breather;
- pattern complexity.

## 10. Fairness

Prototype phải ưu tiên fairness:
- hazard được telegraph đủ sớm;
- collision forgiving hơn visual nếu playtest cần;
- movement frame-rate independent;
- cảm giác điều khiển phải nhất quán trên 60/120Hz;
- seed có thể cố định để tái hiện pattern khi debug;
- không có tình huống procedural bất khả thi.

## 11. Game Feel Prototype

Chỉ dùng feedback cần thiết để đánh giá mechanic:
- graze ring/flash;
- haptic;
- SFX graze;
- death feedback;
- screen shake nhẹ;
- micro slow-motion cho graze rất sát nếu playtest cho thấy không gây khó chịu.

Các con số timing không khóa trước, phải tune trên thiết bị thật.

## 12. Prototype Scope

### Có
- Unity project mobile portrait.
- Relative drag controller.
- Player placeholder.
- 2 obstacle types.
- Object pooling.
- Hit zone + graze zone.
- Proximity scoring.
- Chain Meter + multiplier.
- Score + high score local.
- 4–6 chunks sau khi control/graze cơ bản pass.
- Difficulty ramp cơ bản.
- Instant restart.
- Basic haptic/SFX/VFX.
- Configurable tuning parameters.
- iOS + Android device builds sớm.

### Không
- Ads.
- IAP.
- Firebase.
- Analytics SDK.
- Crashlytics.
- Remote Config.
- Consent/UMP.
- Shop.
- Currency economy.
- Skins.
- Daily missions.
- Leaderboard.
- Backend/cloud.
- Final art/music.

## 13. Technology Decision hiện tại

- Engine: **Unity 6.3 LTS**.
- Rendering: **URP 2D Renderer**.
- Language: C#.
- Target: iOS + Android.
- Orientation: portrait.
- Prototype: zero third-party SDK.

Không dùng DOTS/ECS/Addressables/DI framework hoặc architecture phức tạp nếu prototype chưa cần.

## 14. Visual Direction sau Gameplay Validation

Hướng hiện tại: **Bioluminescent Minimalism**.
- nền tối;
- palette hạn chế;
- hazard dễ đọc;
- player có silhouette/personality;
- VFX không che gameplay;
- tránh generic Unity neon asset-store look.

Chưa làm art hoàn chỉnh trước khi core gameplay pass.

## 15. Validation Gates

### Gate 0 — Touch Feel
Không tiếp tục nếu relative drag chưa tạo cảm giác chính xác, dễ kiểm soát và không che player.

### Gate 1 — Player Validation
Quan sát 8–15 người, không giải thích quá mức. Theo dõi:
- số run tự nguyện;
- người chơi có chủ động graze hay chỉ né xa;
- có hiểu lý do chết không;
- có tự muốn thử lại không;
- có cảm thấy kỹ năng tăng qua vài run không.

Kết quả: CONTINUE / ITERATE / KILL-PIVOT.

### Gate 2 — Viewer / Acquisition Validation
Sau Art Direction v0, dùng clip 10–15 giây để kiểm tra:
- người xem có nhận ra tình huống nguy hiểm không;
- close-call có hấp dẫn không;
- gameplay có dễ hiểu bằng mắt không;
- có ý định muốn thử game không.

## 16. Retention & Monetization — chưa triển khai

Chỉ thiết kế sâu sau khi gameplay pass.

Hướng ứng viên hiện tại:
- Retention: modifier/variation trước, sau đó mới đánh giá currency, missions, cosmetics.
- Ads: rewarded-heavy, interstitial rất có kiểm soát.
- IAP: có thể dùng một gói Remove Ads/Premium nếu economics hợp lý.
- Không subscription trừ khi sản phẩm sau này thay đổi đáng kể.

## 17. Policy Principles

Trước monetization/release phải kiểm tra lại policy hiện hành từ nguồn chính thức:
- Apple App Review Guidelines, đặc biệt 4.2/4.3, IAP, privacy, age rating, ATT khi thuộc phạm vi.
- Google Play repetitive content, Ads, Billing, Data Safety, Target Audience.
- AdMob consent/CMP, invalid traffic, placement.

Không dựa vào giả định policy cũ.

## 18. Commercial Risk chính

Rủi ro lớn nhất hiện tại:
1. Core Graze không đủ vui trên cảm ứng mobile.
2. Người chơi đại chúng chọn né xa và không muốn mạo hiểm.
3. Visual không đủ hấp dẫn để hỗ trợ organic/social acquisition.
4. Game quá tối giản/generic khi tiến tới store.
5. Distribution yếu dù gameplay tốt.

Các rủi ro này phải được kiểm chứng trước khi đầu tư production sâu.

## 19. Current Status

**Status: READY FOR MICRO PROTOTYPE PREPARATION**

Bước kế tiếp: chuẩn bị Unity environment và giao AI coding tạo baseline project + Gate 0 touch-control prototype.
