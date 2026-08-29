# AI_CODING_RULES.md — Brinkshift

Tài liệu này dành cho Cursor, Codex, Claude hoặc AI coding khác khi trực tiếp sửa repository.

## 1. Vai trò

AI coding chịu trách nhiệm:
- tạo/sửa code;
- chạy command;
- build;
- test kỹ thuật;
- xử lý bug;
- commit/checkpoint;
- báo cáo kết quả.

AI coding **không tự ý thay đổi lớn** về:
- core gameplay;
- monetization;
- engine;
- architecture lớn;
- privacy;
- business model;
- store-policy strategy.

Nếu phát hiện vấn đề thiết kế lớn: dừng phần thay đổi đó và báo cáo.

## 2. Nguồn sự thật

Ưu tiên đọc trước:
1. `PROJECT_SPEC.md`
2. `ROADMAP.md`
3. `PLAYTEST_CHECKLIST.md`
4. file này

Spec là living document. Không suy diễn quyết định cũ nếu tài liệu hiện tại đã thay đổi.

## 3. Nguyên tắc prototype

Prototype hiện tại nhằm chứng minh gameplay, không phải làm game hoàn chỉnh.

Không tự thêm:
- Ads;
- IAP;
- Firebase;
- Analytics SDK;
- backend;
- login;
- cloud save;
- shop;
- skins;
- daily missions;
- leaderboard;
- Addressables/DOTS/ECS/DI framework.

## 4. Technology baseline

- Unity 6.3 LTS.
- URP 2D Renderer.
- C#.
- Portrait mobile.
- iOS + Android.
- Zero third-party SDK trong prototype.

Nếu version hoặc package cần thay đổi, phải báo trước lý do.

## 5. Code style

Ưu tiên code đơn giản, đọc được, ít abstraction.

Không over-engineer.

Tách hợp lý các trách nhiệm chính như:
- player/input;
- obstacle/spawning;
- graze/scoring;
- game state/restart;
- tuning data.

Không tạo framework riêng nếu vài MonoBehaviour rõ ràng là đủ.

## 6. Tuning

Các tham số ảnh hưởng game feel phải configurable, không hard-code rải rác:
- sensitivity;
- movement speed;
- hit radius;
- graze radius;
- score curve;
- chain decay;
- obstacle speed;
- spawn spacing;
- difficulty values;
- feedback timing/intensity.

Ưu tiên một cấu hình tuning rõ ràng, ví dụ ScriptableObject khi phù hợp.

## 7. Performance

- Dùng pooling cho obstacle/spawn lặp lại.
- Tránh Instantiate/Destroy liên tục trong gameplay nếu gây GC spike.
- Không tối ưu vi mô vô ích trước khi đo.
- Build/test máy thật sớm.
- Chú ý khác biệt 60/120Hz.

## 8. Input

Relative drag phải:
- không teleport khi touch begin/re-touch;
- không phụ thuộc frame rate;
- cho phép tune sensitivity;
- không để player bị ngón tay che;
- xử lý safe area/screen bounds hợp lý.

Không mặc định FixedUpdate là giải pháp nếu không phù hợp với input pipeline; mục tiêu là cảm giác nhất quán trên thiết bị thật.

## 9. Procedural safety

Không random vô kiểm soát.

Chunk phải có compatibility/safety constraints. Nếu thêm generator, luôn nghĩ tới khả năng sinh tình huống không thể né.

Nên hỗ trợ deterministic seed/debug reproduction khi phù hợp.

## 10. Git

Trước thay đổi lớn: tạo checkpoint.

Sau milestone ổn định:
- commit rõ ràng;
- không commit cache/build artifacts;
- không commit secret/token/credential;
- repo phải sạch sau khi hoàn tất nếu có thể.

Không force-push/rewrite lịch sử nếu không có yêu cầu rõ.

## 11. Security

Không commit:
- API secrets;
- signing keys;
- provisioning secrets;
- private tokens;
- passwords;
- service-account credentials.

Repo hiện public, vì vậy đặc biệt thận trọng.

## 12. Báo cáo nghiệm thu bắt buộc sau milestone

Báo cáo ngắn gọn:
1. File đã tạo/sửa.
2. Chức năng đã hoàn thành.
3. Test đã chạy.
4. Build đã chạy và target nào.
5. Lỗi/cảnh báo còn lại.
6. Package/SDK mới (nếu có).
7. Thay đổi architecture (nếu có).
8. Commit SHA/checkpoint.
9. Hướng dẫn người dùng test trên máy thật.

Không tự tuyên bố gameplay “vui” hoặc “đạt” chỉ vì code chạy. Gameplay phải do playtest xác nhận.
