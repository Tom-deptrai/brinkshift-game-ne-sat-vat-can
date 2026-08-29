# ROADMAP.md — Brinkshift

> Roadmap linh hoạt. Thứ tự có thể thay đổi nếu playtest, policy, dữ liệu hoặc kỹ thuật cho thấy phương án tốt hơn.

## Phase 0 — Repository & Specification
**Mục tiêu:** tạo nền tảng rõ ràng cho AI coding.

- [x] GitHub repository created.
- [x] Unity `.gitignore` present.
- [x] README initialized.
- [x] PROJECT_SPEC initialized.
- [x] AI coding rules defined.
- [x] Playtest checklist defined.

## Phase 1 — Environment & Unity Baseline
**Mục tiêu:** project Unity sạch, build được iOS/Android, chưa cần gameplay hoàn chỉnh.

- Xác nhận Unity Hub.
- Cài Unity 6.3 LTS.
- Cài iOS Build Support.
- Cài Android Build Support + SDK/NDK/OpenJDK theo Unity Hub.
- Tạo project 2D URP tên `Brinkshift`.
- Portrait only.
- Tạo baseline scene.
- Commit/tag baseline ổn định.

**Gate:** project mở sạch, không console error, build được ít nhất một device target.

## Phase 2 — Gate 0: Touch Control Prototype
**Mục tiêu:** chứng minh relative drag chơi tốt trên điện thoại.

- Player placeholder.
- Relative drag controller.
- Configurable sensitivity.
- Screen bounds mềm/hợp lý.
- Frame-rate independent movement.
- Một obstacle đơn giản.
- Instant restart.
- Build lên iPhone + Android thật.

**PASS khi:** điều khiển chính xác, không bị ngón tay che, không có cảm giác trễ/teleport/unfair.

## Phase 3 — Graze Core
**Mục tiêu:** trả lời câu hỏi “Graze có vui không?”

- Hit zone.
- Graze zone.
- Anti-farming rule.
- Proximity scoring.
- Chain Meter decay.
- Multiplier.
- Graze feedback: SFX/haptic/ring/micro slow-mo nếu phù hợp.

**Gate:** người chơi chủ động muốn đi sát hazard thay vì chỉ né xa.

## Phase 4 — Procedural Prototype
**Mục tiêu:** tạo các run khác nhau nhưng công bằng.

- 2 obstacle archetypes.
- Object pooling.
- 4–6 hand-authored chunks.
- Difficulty weights/tags.
- Entry/exit compatibility.
- Seed cố định để debug.
- Difficulty ramp.
- Speed cap/telegraph constraints.

## Phase 5 — Player Validation
**Mục tiêu:** quyết định có tiếp tục concept hay không.

- Test 8–15 người.
- Không giải thích gameplay quá mức.
- Ghi số run tự nguyện.
- Ghi thời gian tới graze chủ ý đầu tiên.
- Ghi cảm nhận khi chết.
- Quyết định: CONTINUE / ITERATE / KILL-PIVOT.

**Không production sâu trước khi gate này pass.**

## Phase 6 — Art Direction v0
**Chỉ làm nếu Player Validation pass.**

- Chốt silhouette player.
- Palette.
- Hazard readability.
- Bioluminescent minimalism v0.
- Visual signature cho close-call.
- Performance test trên Android thật.

## Phase 7 — Viewer / Acquisition Validation
**Mục tiêu:** kiểm chứng watchability và hook.

- Quay 3 clip 10–15 giây.
- Test người chỉ xem, không chơi.
- Kiểm tra: hiểu gameplay, nhận ra close-call, muốn thử game.
- Nếu gameplay pass nhưng video fail: sửa presentation trước, không phá core mechanic vội.

## Phase 8 — Distribution Decision
**Mục tiêu:** chọn chiến lược ra thị trường phù hợp với dữ liệu lúc đó.

Đánh giá:
- tự phát hành global;
- social/short-form organic;
- editorial featuring;
- publisher nếu thực sự có lợi.

Không khóa chiến lược distribution từ bây giờ.

## Phase 9 — Core Production

- Tăng số chunk/hazard.
- Sawtooth pacing.
- Difficulty tuning.
- Save/high score.
- Audio/game feel polish.
- Performance.
- Accessibility cơ bản.

## Phase 10 — Retention

Ưu tiên đánh giá:
1. run modifiers / variation;
2. simple progression;
3. missions;
4. character personality/cosmetic;
5. daily/sector systems nếu có bằng chứng cần thiết.

Không thêm meta chỉ để làm game “nhiều chức năng”.

## Phase 11 — Monetization + SDK

Sau khi gameplay/retention có cơ sở:
- Analytics.
- Crashlytics.
- AdMob.
- CMP/consent khi cần.
- Rewarded placements.
- Capped interstitial.
- IAP/Premium/Remove Ads nếu phù hợp.
- Remote Config nếu đem lại giá trị rõ.

Tích hợp từng SDK một và build/test sau mỗi SDK.

## Phase 12 — QA & Compliance

- iPhone + Android thật.
- 60/120Hz.
- low/mid-range Android nếu có.
- background/resume.
- network loss.
- Ads unavailable.
- purchase/restore.
- crash/ANR.
- Apple/Google/Ads/Privacy/IAP/IP audit.

## Phase 13 — Store Identity & ASO

- Final name conflict audit.
- Icon.
- Screenshots.
- Preview video.
- Subtitle/short description.
- Keywords dựa trên dữ liệu thật, không đoán search volume.
- Localization theo thị trường ưu tiên.

## Phase 14 — Testing / Soft Launch

Nếu phù hợp:
- TestFlight.
- Google testing track.
- Limited market/soft launch trước global release.
- Đo D1/D7/session/playtime/crash/store conversion.

## Phase 15 — Global Release

Chỉ release khi compliance audit không còn FAIL nghiêm trọng.

## Phase 16 — Post-launch Optimization

Theo dõi:
- installs;
- store conversion;
- D1/D7;
- sessions/user;
- playtime;
- death points;
- graze behavior;
- rewarded acceptance;
- ad revenue;
- IAP conversion;
- crash/ANR;
- countries/reviews.

Quyết định: tối ưu, mở rộng, pivot hoặc dừng đầu tư.
